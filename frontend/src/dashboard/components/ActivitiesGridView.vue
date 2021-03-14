<template>
  <div id="activitiesGrid" class="card">
    <div class="card-header font-weight-bold">Review your activities</div>
    <div class="card-body">
      <section v-if="activitiesState.errored">
        <p>
          We're sorry, we're not able to retrieve this information at the
          moment, please try back later
        </p>
      </section>

      <section v-else>
        <div v-if="activitiesState.loading">Loading...</div>

        <form id="search">
          <div class="form-row">
            <div class="form-group col-md-3">
              <label for="activityType">Type</label>
              <select
                id="activityType"
                class="form-control"
                v-model="selectedActivityType"
              >
                <option :selected="true">All</option>
                <option
                  v-for="type in activitiesState.types"
                  :key="type"
                  v-bind:value="type"
                >
                  {{ type }}
                </option>
              </select>
            </div>

            <div class="form-group col-md-3">
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

            <div class="form-group col-md-3">
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
        </form>

        <GridView :data="filteredActivities" :columns="columns" />
      </section>
    </div>
  </div>
</template>

<script>
import activitiesStore from '../../store/activitiesStore';
import GridView from './GridView.vue';

export default {
  name: 'ActivitiesGridView',
  components: {
    GridView,
  },
  data() {
    return {
      activitiesState: activitiesStore.state,
      columns: [
        'id',
        'type',
        'name',
        'localDate',
        'utcDate',
        'timezone',
        'movingTime',
        'elapsedTime',
        'distance',
        'averageSpeed',
        'maxSpeed',
        'elevationGain',
        'elevationHigh',
        'elevationLow',
      ],
      selectedActivityType: 'All',
      fromDate: null,
      throughDate: null,
    };
  },
  computed: {
    filteredActivities() {
      let activities = this.activitiesState.data;

      const selectedValue = this.selectedActivityType;

      if (selectedValue && selectedValue !== 'All') {
        activities = activities.filter((activity) => activity.type === selectedValue);
      }

      const { fromDate } = this.fromDate;

      if (fromDate) {
        activities = activities.filter((activity) => activity.localDate.substr(0, 10) >= fromDate);
      }

      const { throughDate } = this.throughDate;

      if (throughDate) {
        activities = activities.filter(
          (activity) => activity.localDate.substr(0, 10) <= throughDate,
        );
      }

      return activities;
    },
  },
};
</script>
