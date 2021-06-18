<template>
  <div id="activitiesGrid" class="card">
    <div class="card-header font-weight-bold">Review your activities</div>
    <div class="card-body">
      <section v-if="error">
        <p>
          We're sorry, we're not able to retrieve this information at the
          moment, please try back later
        </p>
      </section>

      <section v-else>
        <div v-if="loading">Loading...</div>

        <div class="mb-2">
          <div class="row">
            <div class="col-md-3">
              <label for="activityType">Type</label>
              <select
                id="activityType"
                class="form-select"
                v-model="selectedType"
              >
                <option :selected="true">All</option>
                <option v-for="type in types" :key="type" v-bind:value="type">
                  {{ type }}
                </option>
              </select>
            </div>

            <div class="col-md-3">
              <label for="activityFromDate">From Date</label>
              <input
                id="activityFromDate"
                class="form-control"
                name="fromDate"
                v-model="fromDate"
                type="date"
                placeholder="From Date"
              />
            </div>

            <div class="col-md-3">
              <label for="activityThroughDate">Through Date</label>
              <input
                id="activityThroughDate"
                class="form-control"
                name="throughDate"
                v-model="throughDate"
                type="date"
                placeholder="Through Date"
              />
            </div>
          </div>
        </div>

        <div class="mb-2">
          <GridView :data="filteredActivities" :columns="columns" />
        </div>
      </section>
    </div>
  </div>
</template>

<script>
import ActivitiesMixIn from '@/mixins/activities-mixin';
import GridView from '@/dashboard/components/GridView.vue';
import ActivityFormatters from '@/helpers/activityFormatters';

export default {
  name: 'ActivitiesGridView',
  components: {
    GridView,
  },
  mixins: [ActivitiesMixIn],
  data() {
    return {
      columns: ['id', 'type', 'name', 'localDate', 'utcDate', 'timezone', 'movingTime', 'elapsedTime', 'distance', 'averageSpeed', 'maxSpeed', 'elevationGain', 'elevationHigh', 'elevationLow'],
      selectedType: 'All',
      fromDate: null,
      throughDate: null,
    };
  },
  computed: {
    filteredActivities() {
      let { activities } = this;

      if (this.selectedType && this.selectedType !== 'All') {
        activities = activities.filter((activity) => activity.type === this.selectedType);
      }

      if (this.fromDate) {
        activities = activities.filter((activity) => activity.localDate.substr(0, 10) >= this.fromDate);
      }

      if (this.throughDate) {
        activities = activities.filter((activity) => activity.localDate.substr(0, 10) <= this.throughDate);
      }

      return ActivityFormatters.formatActivities(activities);
    },
  },
};
</script>
