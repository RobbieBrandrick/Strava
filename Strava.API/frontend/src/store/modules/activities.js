import axios from 'axios';

export default {
  namespaced: true,
  state: {
    data: [],
    loading: true,
    errored: false,
    types: [],
  },
  getters: {},
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
            activity.efforts = 1;
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
