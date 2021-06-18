<template id="RidingComponent">
  <div class="row">
    <div class="col-xl-8">
      <ActivityChartComponent type="Ride" v-bind:columns="['distance', 'movingTime']">Cycling Chart</ActivityChartComponent>
    </div>
    <div class="col-xl-4"></div>
  </div>
</template>

<script>
import ActivityChartComponent from '@/dashboard/ActivityChartComponent/ActivityChartComponent.vue';

export default {
  name: 'RidingComponent',
  components: {
    ActivityChartComponent,
  },
};
</script>
