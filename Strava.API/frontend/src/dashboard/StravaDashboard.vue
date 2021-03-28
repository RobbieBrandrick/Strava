<template>
  <div>
    <ModifyActivities />

    <div class="text-center">
      <h1 class="display-4">Dashboard</h1>
    </div>
    <CardioViews/>
    <ActivitiesGridView/>
  </div>
</template>

<script>
import ActivitiesGridView from './components/ActivitiesGridView.vue';
import CardioViews from './components/CardioViews.vue';
import ModifyActivities from './components/ModifyActivities.vue';

export default {
  name: 'StravaDashboard',
  components: {
    CardioViews,
    ActivitiesGridView,
    ModifyActivities,
  },
  data() {
    return {};
  },

};
</script>
