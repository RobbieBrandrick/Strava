<template id="RunningComponent">
  <div class="card">
    <div class="card-header">Running</div>
    <div class="card-body">
      <div class="row">
        <div class="col-xl-8">
          <ActivityChartComponent
            type="Run"
            v-bind:columns="['distance', 'movingTime']"
          />
        </div>
        <div class="col-xl-4">
          <ActivityStatisticsComponents type="Run" />
        </div>
      </div>
    </div>
  </div>
</template>

<script>
import ActivityChartComponent from '@/dashboard/ActivityChartComponent/ActivityChartComponent.vue';
import ActivityStatisticsComponents from '@/dashboard/ActivityStatisticsComponent/ActivityStatisticsComponent.vue';

export default {
  name: 'RunningComponent',
  components: {
    ActivityChartComponent,
    ActivityStatisticsComponents,
  },
};
</script>
