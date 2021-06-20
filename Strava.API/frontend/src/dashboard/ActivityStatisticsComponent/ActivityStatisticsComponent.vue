<template>
  <div>
    <AverageStatisticsComponent type="type" />
  </div>
</template>

<script>
import AverageStatisticsComponent from './AverageStatisticsComponent.vue';

export default {
  name: 'ActivityStatisticsComponent',
  components: {
    AverageStatisticsComponent,
  },
  props: {
    type: String,
  },
};
</script>
