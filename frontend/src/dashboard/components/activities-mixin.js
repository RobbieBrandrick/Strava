export default {
  created() {
    this.$store.dispatch('activities/get');
  },
  computed: {
    activities() {
      return this.$store.state.activities.data;
    },
    types() {
      return this.$store.state.activities.types;
    },
    loading() {
      return this.$store.state.activities.loading;
    },
    error() {
      return this.$store.state.activities.error;
    },
  },
};
