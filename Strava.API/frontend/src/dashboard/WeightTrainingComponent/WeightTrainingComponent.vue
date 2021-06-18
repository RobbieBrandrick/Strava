<template id="WeightTrainingComponent">
  <div class="row">
    <div class="col-xl-8">
      <ActivityChartComponent type="WeightTraining" v-bind:columns="['efforts']">WeightTrainingComponent Chart</ActivityChartComponent>
    </div>
    <div class="col-xl-4">

    </div>
  </div>
</template>

<script>
import ActivityChartComponent from '@/dashboard/ActivityChartComponent/ActivityChartComponent.vue';

export default {
  name: 'WeightTrainingComponent',
  components: {
    ActivityChartComponent,
  },
};
</script>
