<template id="RidingComponent">
  <div class="card">
    <div class="card-header">Cycling</div>
    <div class="card-body">
      <div class="row">
        <div class="col-xl-8">
          <ActivityChartComponent
            type="Ride"
            v-bind:columns="['distance', 'movingTime']"
          />
        </div>
        <div class="col-xl-4">
          <ActivityStatisticsComponents type="Ride" />
        </div>
      </div>
    </div>
  </div>
</template>

<script>
import ActivityChartComponent from '@/dashboard/ActivityChartComponent/ActivityChartComponent.vue';
import ActivityStatisticsComponents from '@/dashboard/ActivityStatisticsComponent/ActivityStatisticsComponent.vue';

export default {
  name: 'RidingComponent',
  components: {
    ActivityChartComponent,
    ActivityStatisticsComponents,
  },
};
</script>
