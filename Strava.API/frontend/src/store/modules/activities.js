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
    formatTime: () => (data) => moment.utc(data * 1000).format('HH:mm:ss'),
    formatDistance: () => (data) => `${(data / 1000).toFixed(2)} km`,
    formatRideSpeed: () => (data) => `${(data * 3.6).toFixed(2)} km/h`,
    formatRunSpeed: () => (data) => {
      if (data === 0) {
        return '-';
      }

      return `${(16.666666667 / data).toFixed(2)} /km`;
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
    formatElevation: () => (data) => `${Math.round(data, 2).toFixed(2)} m`,
    groupByDate: (state, getters) => (data, type, column) => {
      const averageColumns = ['maxSpeed', 'averageSpeed'];

      if (averageColumns.includes(column)) {
        return getters.groupByDateAvg(data, type, column);
      }

      return getters.groupByDateSum(data, type, column);
    },
    groupByDateSum: (state, getters) => (data, type, column) => {
      const result = new Map();

      data.forEach((activity) => {
        const date = activity.localDate.substr(0, 10);
        const dateKey = getters.getDateKey(date, type);

        if (!result.has(dateKey)) {
          result.set(dateKey, 0);
        }

        const newSum = result.get(dateKey) + activity[column];

        result.set(dateKey, newSum);
      });

      return result;
    },
    groupByDateAvg: (state, getters) => (data, type, column) => {
      const result = new Map();

      data.forEach((activity) => {
        const date = activity.localDate.substr(0, 10);
        const dateKey = getters.getDateKey(date, type);

        if (!result.has(dateKey)) {
          result.set(dateKey, activity[column]);
        }

        const newAvg = ((result.get(dateKey) + activity[column]) / 2);

        result.set(dateKey, newAvg);
      });

      return result;
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
  },
};
