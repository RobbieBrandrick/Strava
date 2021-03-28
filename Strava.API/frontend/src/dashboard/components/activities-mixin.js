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
    ...mapActions('activities', ['get']),
    ...mapGetters('activities', {
      formatActivities: 'formatActivities',
      groupBy: 'groupByDate',
      formatTime: 'formatTime',
      formatDistance: 'formatDistance',
      formatSpeed: 'formatSpeed',
      formatElevation: 'formatElevation',
    }),
  },
};
