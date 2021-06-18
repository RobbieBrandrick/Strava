<template id="RunningComponent">
  <div class="row">
    <div class="col-xl-8">
      <ActivityChartComponent type="Run" v-bind:columns="['distance', 'movingTime']">Running Chart</ActivityChartComponent>
    </div>
    <div class="col-xl-4">

    </div>
  </div>
</template>

<script>
import ActivityChartComponent from '@/dashboard/ActivityChartComponent/ActivityChartComponent.vue';

export default {
  name: 'RunningComponent',
  components: {
    ActivityChartComponent,
  },
};
</script>
