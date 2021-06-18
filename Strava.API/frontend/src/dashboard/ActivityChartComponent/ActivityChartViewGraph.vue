<template>
  <div class="card mt-2 mb-2 p-2">
    <table class="table">
      <thead>
        <tr>
          <th scope="col">Time</th>
          <th scope="col">Distance</th>
          <th scope="col">Elev Gain</th>
          <th scope="col">Efforts</th>
        </tr>
      </thead>
      <tbody>
        <tr>
          <td>{{ graphData.time }}</td>
          <td>{{ graphData.distance }}</td>
          <td>{{ graphData.elevation }}</td>
          <td>{{ graphData.efforts }}</td>
        </tr>
      </tbody>
    </table>
  </div>
</template>

<script>
import ActivitiesMixIn from '@/mixins/activities-mixin';
import ActivityFormatters from '@/helpers/activityFormatters';

export default {
  name: 'ActivityChartViewGraph',
  mixins: [ActivitiesMixIn],
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

      time = ActivityFormatters.formatTime(time);

      let distance = result
        .map((e) => e.distance)
        .reduce((previousValue, currentValue) => previousValue + currentValue, 0);

      distance = ActivityFormatters.formatDistance(distance);

      let elevation = result
        .map((e) => e.elevationGain)
        .reduce((previousValue, currentValue) => previousValue + currentValue, 0);

      elevation = ActivityFormatters.formatElevation(elevation);

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
