import { mapState, mapActions, mapGetters } from 'vuex';

export default {
  created() {
    this.get();
  },
  computed: {
    ...mapState('activities', {
      activities: 'data',
      types: 'types',
      loading: 'loading',
      error: 'error',
    }),
  },
  methods: {
    ...mapActions('activities', ['get', 'refreshActivities']),
    ...mapGetters('activities', {}),
  },
};
