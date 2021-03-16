<template>
  <div>
    <div class="text-center">
      <h1 class="display-4">Dashboard</h1>
    </div>
    <CardioCharts />
    <ActivitiesGridView />
  </div>
</template>

<script>
import ActivitiesGridView from './components/ActivitiesGridView.vue';
import CardioCharts from './components/CardioCharts.vue';

export default {
  name: 'StravaDashboard',
  components: {
    CardioCharts,
    ActivitiesGridView,
  },
  data() {
    return {};
  },
};
</script>
