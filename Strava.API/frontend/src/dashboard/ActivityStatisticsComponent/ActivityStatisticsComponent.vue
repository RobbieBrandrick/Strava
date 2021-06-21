<template>
  <div class="card">
    <div class="card-body">
      <h1>Stats</h1>
      <hr />
      <AverageStatisticsComponent :type="this.type" />
      <YTDStatisticsComponent :type="this.type" />
      <ATStatisticsComponent :type="this.type" />
    </div>
  </div>
</template>

<script>
import AverageStatisticsComponent from './AverageStatisticsComponent.vue';
import YTDStatisticsComponent from './YearToDateStatisticsComponent.vue';
import ATStatisticsComponent from './AllTimeStatisticsComponent.vue';

export default {
  name: 'ActivityStatisticsComponent',
  components: {
    AverageStatisticsComponent,
    YTDStatisticsComponent,
    ATStatisticsComponent,
  },
  props: {
    type: String,
  },
};
</script>
