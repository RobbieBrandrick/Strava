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
    groupByDate: (state, getters) => (data, type, key) => {
      const result = new Map();

      data.forEach((activity) => {
        const date = activity.localDate.substr(0, 10);
        const dateKey = getters.getDateKey(date, type);

        if (!result.has(dateKey)) {
          result.set(dateKey, 0);
        }

        const newDistance = result.get(dateKey) + activity[key];

        result.set(dateKey, newDistance);
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

      axios.get('https://localhost:5001/Activity', { crossdomain: true })
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
