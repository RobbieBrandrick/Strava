<template>
  <div class="text-center">
    <h1 class="display-4">Dashboard</h1>
  </div>
</template>

<script>

import activitiesStore from '../store/activitiesStore';

export default {
  name: 'StravaDashboard',
  mounted() {
    activitiesStore.loadActivities();
  },
};
</script>
