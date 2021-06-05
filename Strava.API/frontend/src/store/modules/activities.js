import axios from 'axios';
import moment from 'moment';

export default {
  namespaced: true,
  state: {
    data: [],
    loading: true,
    errored: false,
    types: [],
  },
  getters: {
    formatActivities: (state, getters) => (data) => {
      const activities = [];

      data.forEach((a) => {
        activities.push({
          id: a.id,
          type: a.type,
          name: a.name,
          localDate: a.localDate,
          utcDate: a.utcDate,
          timezone: a.timezone,
          movingTime: getters.formatTime(a.movingTime),
          elapsedTime: getters.formatTime(a.elapsedTime),
          distance: getters.formatDistance(a.distance),
          averageSpeed: getters.formatSpeed(a.averageSpeed, a.type),
          maxSpeed: getters.formatSpeed(a.maxSpeed, a.type),
          elevationGain: getters.formatElevation(a.elevationGain),
          elevationHigh: getters.formatElevation(a.elevationHigh),
          elevationLow: getters.formatElevation(a.elevationLow),
        });
      });
      return activities;
    },
    formatTime: (state, getters) => (data) => `${getters.formatHours(data)}:${getters.formatMinutes(data)}:${getters.formatSeconds(data)}`,
    formatHours: () => (data) => Math.round(Math.floor(((data / 60) / 60)) % 60).toString().padStart(2, '0'),
    formatMinutes: () => (data) => Math.round((data / 60) % 60).toString().padStart(2, '0'),
    formatSeconds: () => (data) => Math.round(data % 60).toString().padStart(2, '0'),
    formatDistance: () => (data) => `${(data / 1000).toFixed(2)} `,
    formatRideSpeed: () => (data) => `${(data * 3.6).toFixed(2).padStart(5, '0')}`,
    formatRunSpeed: () => (data) => {
      if (data === 0) {
        return '-';
      }

      return `${(16.666666667 / data).toFixed(2)}`;
    },
    formatSpeed: (state, getters) => (data, type) => {
      if (type === 'Run') {
        return getters.formatRunSpeed(data);
      }
      if (type === 'Ride') {
        return getters.formatRideSpeed(data);
      }
      return data;
    },
    formatElevation: () => (data) => `${Math.round(data, 2).toFixed(2).toString().padStart(6, '0')}`,
    groupByDate: (state, getters) => (data, type, column, fillInDates) => {
      const averageColumns = ['maxSpeed', 'averageSpeed'];

      const result = new Map();

      if (fillInDates) {
        getters.applyFillInDates(result, data, type);
      }

      if (averageColumns.includes(column)) {
        getters.groupByDateAvg(result, data, type, column);
      } else {
        getters.groupByDateSum(result, data, type, column);
      }
      return result;
    },
    groupByDateSum: (state, getters) => (result, data, type, column) => {
      data.forEach((activity) => {
        const date = activity.localDate.substr(0, 10);
        const dateKey = getters.getDateKey(date, type);

        if (!result.has(dateKey)) {
          result.set(dateKey, { data: 0, dates: [] });
        }

        const record = result.get(dateKey);

        record.data += activity[column];
        record.dates.push(date);

        result.set(dateKey, record);
      });

      return result;
    },
    groupByDateAvg: (state, getters) => (result, data, type, column) => {
      data.forEach((activity) => {
        const date = activity.localDate.substr(0, 10);
        const dateKey = getters.getDateKey(date, type);

        if (!result.has(dateKey)) {
          result.set(dateKey, { data: activity[column], dates: [] });
        }

        const record = result.get(dateKey);

        record.data = ((record.data + activity[column]) / 2);
        record.dates.push(date);

        result.set(dateKey, record);
      });

      return result;
    },
    applyFillInDates: (state, getters) => (result, data, type) => {
      const dates = data.map((e) => e.localDate.substr(0, 10));
      const startDate = moment(dates.sort((lhs, rhs) => lhs > rhs)[0]);
      const endDate = moment(dates.sort((lhs, rhs) => lhs < rhs)[0]);
      let incrementDateBy;

      if (type === 'Day') {
        incrementDateBy = 'days';
      } else if (type === 'Week') {
        incrementDateBy = 'weeks';
      } else if (type === 'Month') {
        incrementDateBy = 'months';
      } else if (type === 'Year') {
        incrementDateBy = 'years';
      }

      while (endDate.diff(startDate, incrementDateBy) > -1) {
        const dateKey = getters.getDateKey(endDate, type);
        if (!result.has(dateKey)) {
          result.set(dateKey, { data: 0, dates: [] });
        }
        endDate.add(-1, incrementDateBy);
      }
    },
    getDateKey: () => (dateStr, type) => {
      const date = moment(dateStr);

      if (type === 'Day') {
        return date.format('YYYY-MM-DD');
      } if (type === 'Week') {
        return date.format('YYYY-wo');
      } if (type === 'Month') {
        return date.format('YYYY-MMMM');
      } if (type === 'Year') {
        return date.format('YYYY');
      }

      return null;
    },
  },
  mutations: {
    setActivities(state, activities) {
      const { data } = state;

      data.splice(0, data.length);
      data.unshift(...activities);
    },
    addActivities(state, activities) {
      const { data } = state;

      data.unshift(...activities);
    },
    removeActivities(state, activities) {
      const { data } = state;

      activities.forEach((e) => {
        const i = data.map((item) => item.id).indexOf(e.id);

        data.splice(i, 1);
      });
    },
    clearActivities(state) {
      const { data } = state;

      localStorage.removeItem('activities');

      data.splice(0);
    },
    setActivityTypes(state, activityTypes) {
      const { types } = state;

      types.splice(0, types.length);
      types.unshift(...activityTypes);
    },
    setActivitiesLoading(state, loading) {
      state.loading = loading;
    },
    setActivitiesError(state, error) {
      state.error = error;
    },
  },
  actions: {
    get({ state, commit }) {
      commit('setActivitiesLoading', true);
      commit('setActivitiesError', false);

      const { data } = state;

      if (localStorage.getItem('activities')) {
        const localStorageData = JSON.parse(localStorage.getItem('activities'));
        commit('setActivities', localStorageData);
      }

      axios.get('/Activity', { crossdomain: true })
        .then((response) => {
          const newActivities = [];
          const existingIds = data.map((e) => e.id);

          for (let i = 0; i < response.data.length; i += 1) {
            const activity = response.data[i];
            if (!existingIds.includes(activity.id)) {
              newActivities.push(activity);
            }
          }

          if (newActivities.length > 0) {
            commit('addActivities', newActivities);
          }

          localStorage.setItem('activities', JSON.stringify(data));
        })
        .then(() => {
          const types = data.map((e) => e.type);
          const uniqueTypes = [...new Set(types)];
          uniqueTypes.push('All');
          commit('setActivityTypes', uniqueTypes);
        })
        .catch((error) => {
          console.info(error);
          commit('setActivitiesError', false);
        })
        .finally(() => { commit('setActivitiesLoading', false); });
    },
    refreshActivities({ commit, dispatch }) {
      commit('clearActivities');
      dispatch('get');
    },
  },
};
