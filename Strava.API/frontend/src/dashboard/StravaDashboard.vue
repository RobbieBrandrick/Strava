<template>
  <div>
    <ModifyActivities />

    <div class="text-center">
      <h1 class="display-4">Dashboard :)</h1>
    </div>
    <RunningComponent />
    <RidingComponent />
    <WeightTrainingComponent />
    <ActivitiesGridView />
  </div>
</template>

<script>
import ActivitiesGridView from './ActivitiesGridViewComponent/ActivitiesGridView.vue';
import ModifyActivities from './ModifyActivitiesComponent/ModifyActivities.vue';
import RunningComponent from './RunningComponent/RunningComponent.vue';
import RidingComponent from './RidingComponent/RidingComponent.vue';
import WeightTrainingComponent from './WeightTrainingComponent/WeightTrainingComponent.vue';

export default {
  name: 'StravaDashboard',
  components: {
    ModifyActivities,
    RunningComponent,
    RidingComponent,
    WeightTrainingComponent,
    ActivitiesGridView,
  },
  data() {
    return {};
  },

};
</script>
