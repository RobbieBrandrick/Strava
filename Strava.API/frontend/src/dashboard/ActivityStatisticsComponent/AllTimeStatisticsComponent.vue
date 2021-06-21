<template>
  <div>
    <div class="d-flex flex-row mb-3">
      <h4 class="pe-2">All-Time</h4>
    </div>
    <hr />
    <table class="table table-sm">
      <tr>
        <th scope="col">Efforts</th>
        <td>{{ data.efforts }}</td>
      </tr>
      <tr>
        <th scope="col">Time</th>
        <td>{{ data.time }}</td>
      </tr>
      <tr>
        <th scope="col">Distance</th>
        <td>{{ data.distance }}</td>
      </tr>
      <tr>
        <th scope="col">Elevation Gain</th>
        <td>{{ data.elevationGain }}</td>
      </tr>
    </table>
  </div>
</template>

<script>
import ActivitiesMixIn from '@/mixins/activities-mixin';
import ActivityFormatters from '@/helpers/activityFormatters';

export default {
  name: 'AllTimeStatisticsComponent',
  mixins: [ActivitiesMixIn],
  props: {
    type: String,
  },
  computed: {
    data() {
      const { activities } = this;
      const activitiesResult = activities.filter((e) => e.type === this.type);

      const efforts = ActivityFormatters.formatEfforts(activitiesResult.length);
      const time = ActivityFormatters.formatTime(activitiesResult.reduce((a, e) => a + e.movingTime, 0));
      const distance = ActivityFormatters.formatDistance(activitiesResult.reduce((a, e) => a + e.distance, 0));
      const elevationGain = ActivityFormatters.formatElevation(activitiesResult.reduce((a, e) => a + e.elevationGain, 0));

      return {
        efforts,
        time,
        distance,
        elevationGain,
      };
    },
  },
};
</script>
