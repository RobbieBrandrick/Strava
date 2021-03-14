<template id="activity-charts">
  <div class="row">
    <div class="col-xl-6">
      <CardioChart type="Run" :group-data="runningState.groupByDate" />
    </div>
    <div class="col-xl-6">
      <CardioChart type="Ride" :group-data="runningState.groupByDate" />
    </div>
  </div>
</template>

<script>
import runningActivityStore from '../../store/runningActivityStore';

import CardioChart from './CardioChart.vue';

export default {
  name: 'ActivityCharts',
  components: {
    CardioChart,
  },
  data() {
    return {
      runningState: runningActivityStore,
    };
  },
};
</script>
