<template>
  <div>
    <div class="card p-2">
      <div class="card-header">
        <div class="text-center">
          <i class="bi bi-emoji-smile"></i>
          <h1 class="display-4">Strava Dashboard :)</h1>
        </div>
      </div>
      <RunningComponent />
      <RidingComponent />
      <WeightTrainingComponent />
      <YogaComponent />
      <ActivitiesGridView />
      <ModifyActivities />
    </div>
  </div>
</template>

<script>
import ActivitiesGridView from './ActivitiesGridViewComponent/ActivitiesGridView.vue';
import ModifyActivities from './ModifyActivitiesComponent/ModifyActivities.vue';
import RunningComponent from './RunningComponent/RunningComponent.vue';
import RidingComponent from './RidingComponent/RidingComponent.vue';
import WeightTrainingComponent from './WeightTrainingComponent/WeightTrainingComponent.vue';
import YogaComponent from './YogaComponent/YogaComponent.vue';

export default {
  name: 'StravaDashboard',
  components: {
    ModifyActivities,
    RunningComponent,
    RidingComponent,
    WeightTrainingComponent,
    YogaComponent,
    ActivitiesGridView,
  },
  data() {
    return {};
  },

};
</script>
