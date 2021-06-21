<template id="WeightTrainingComponent">
  <div class="card">
    <div class="card-header">Weight Training</div>
    <div class="card-body">
      <div class="row">
        <div class="col-xl-8">
          <ActivityChartComponent
            type="WeightTraining"
            v-bind:columns="['efforts']"
          />
        </div>
        <div class="col-xl-4">
          <ActivityStatisticsComponents type="WeightTraining" />
        </div>
      </div>
    </div>
  </div>
</template>

<script>
import ActivityChartComponent from '@/dashboard/ActivityChartComponent/ActivityChartComponent.vue';
import ActivityStatisticsComponents from '@/dashboard/ActivityStatisticsComponent/ActivityStatisticsComponent.vue';

export default {
  name: 'WeightTrainingComponent',
  components: {
    ActivityChartComponent,
    ActivityStatisticsComponents,
  },
};
</script>
