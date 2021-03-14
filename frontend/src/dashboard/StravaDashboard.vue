<template>
  <div>
    <div class="text-center">
      <h1 class="display-4">Dashboard</h1>
    </div>
    <ActivitiesGridView />
  </div>
</template>

<script>
import activitiesStore from '../store/activitiesStore';
import ActivitiesGridView from './components/ActivitiesGridView.vue';

export default {
  name: 'StravaDashboard',
  components: {
    ActivitiesGridView,
  },
  mounted() {
    activitiesStore.loadActivities();
  },
};
</script>
