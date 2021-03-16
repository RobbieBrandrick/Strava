import Vue from 'vue';
import Vuex from 'vuex';

import activitiesModule from './modules/activities';

Vue.use(Vuex);

export default new Vuex.Store({
  modules: {
    activities: activitiesModule,
  },
});
