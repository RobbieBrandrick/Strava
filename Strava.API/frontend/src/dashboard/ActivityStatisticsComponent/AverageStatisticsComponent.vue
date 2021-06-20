<template>
  <div>
    <h2>Average</h2>
    <label>Week</label>
    <select class="form-select" v-model="weeks">
      <option v-for="week in this.getWeeksFilter()" :key="week" v-bind:value="week">
        {{ week }}
      </option>
    </select>
    <table>
      <tr>
        <th>Efforts</th>
        <td>{{ data.efforts }}</td>
      </tr>
      <tr>
        <th>Time</th>
        <td>{{ data.time }}</td>
      </tr>
      <tr>
        <th>Distance</th>
        <td>{{ data.distance }}</td>
      </tr>
      <tr>
        <th>Elevation Gain</th>
        <td>{{ data.elevationGain }}</td>
      </tr>
    </table>
  </div>
</template>

<script>
import moment from 'moment';

import ActivitiesMixIn from '@/mixins/activities-mixin';
import ActivityFormatters from '@/helpers/activityFormatters';

export default {
  name: 'AverageStatisticsComponent',
  mixins: [ActivitiesMixIn],
  props: {
    type: String,
  },
  data() {
    return {
      weeks: 4,
    };
  },
  computed: {
    data() {
      const { activities } = this;
      const fromDate = moment().subtract(4, 'weeks').format('YYYY-MM-DD');
      const throughDate = moment().format('YYYY-MM-DD');

      let activitiesResult = activities.filter((e) => e.type === this.type);

      if (fromDate) {
        activitiesResult = activitiesResult.filter(
          (activity) => activity.localDate.substr(0, 10) >= fromDate,
        );
      }

      if (throughDate) {
        activitiesResult = activitiesResult.filter(
          (activity) => activity.localDate.substr(0, 10) <= throughDate,
        );
      }

      const efforts = activitiesResult.length / this.weeks;
      const time = ActivityFormatters.formatTime(activitiesResult.reduce((a, e) => a + e.movingTime, 0) / this.weeks);
      const distance = ActivityFormatters.formatDistance(activitiesResult.reduce((a, e) => a + e.distance, 0) / this.weeks);
      const elevationGain = ActivityFormatters.formatElevation(activitiesResult.reduce((a, e) => a + e.elevationGain, 0) / this.weeks);

      return {
        efforts,
        time,
        distance,
        elevationGain,
      };
    },
  },
  methods: {
    getWeeksFilter() {
      const result = [];

      for (let i = 4; i <= 52; i += 4) {
        result.push(i);
      }

      return result;
    },
  },
};
</script>
