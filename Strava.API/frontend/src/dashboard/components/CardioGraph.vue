<template>
  <div>
    <table class="table">
      <thead>
        <td>Time</td>
        <td>Distance</td>
        <td>Elev Gain</td>
        <td>Efforts</td>
      </thead>
      <tbody>
        <td>
          {{ graphData.time }}
        </td>
        <td>{{ graphData.distance }}</td>
        <td>{{ graphData.elevation }}</td>
        <td>{{ graphData.efforts }}</td>
      </tbody>
    </table>
  </div>
</template>

<script>
import getActivitiesMixin from './activities-mixin';

export default {
  name: 'CardioGraph',
  mixins: [getActivitiesMixin],
  props: {
    fromDate: String,
    throughDate: String,
    type: String,
  },
  computed: {
    graphData() {
      const { activities } = this;
      let result = activities.filter((e) => e.type === this.type);

      if (this.fromDate) {
        result = result.filter(
          (activity) => activity.localDate.substr(0, 10) >= this.fromDate,
        );
      }

      if (this.throughDate) {
        result = result.filter(
          (activity) => activity.localDate.substr(0, 10) <= this.throughDate,
        );
      }

      let time = result
        .map((e) => e.movingTime)
        .reduce((previousValue, currentValue) => previousValue + currentValue, 0);

      time = this.formatTime()(time);

      let distance = result
        .map((e) => e.distance)
        .reduce((previousValue, currentValue) => previousValue + currentValue, 0);

      distance = this.formatDistance()(distance);

      let elevation = result
        .map((e) => e.elevationGain)
        .reduce((previousValue, currentValue) => previousValue + currentValue, 0);

      elevation = this.formatElevation()(elevation);

      const efforts = result
        .length;

      return {
        time,
        distance,
        elevation,
        efforts,
      };
    },
  },
};
</script>
