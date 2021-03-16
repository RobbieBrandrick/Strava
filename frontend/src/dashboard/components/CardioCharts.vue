<template id="activity-charts">
  <div class="row">
    <div class="col-xl-6">
      <CardioChart type="Run">
        Review your running activities
      </CardioChart>
    </div>
    <div class="col-xl-6">
      <CardioChart type="Ride">
        Review your cycling activities
      </CardioChart>
    </div>
  </div>
</template>

<script>
import CardioChart from './CardioChart.vue';

export default {
  name: 'ActivityCharts',
  components: {
    CardioChart,
  },
};
</script>
