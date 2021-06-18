<template>
  <div class="card">
    <div class="card-body">
      <div class="row mb-2">
        <div class="col-md-3">
          <label :for="'groupByDate' + ucid">Group By</label>
          <select
            :id="'groupByDate' + ucid"
            class="form-select"
            v-model="selectedGroupByDate"
          >
            <option>Day</option>
            <option>Week</option>
            <option>Month</option>
            <option>Year</option>
          </select>
        </div>

        <div class="col-md-3">
          <label :for="'activityFromDate' + ucid">From Date</label>
          <input
            :id="'activityFromDate' + ucid"
            class="form-control"
            name="fromDate"
            v-model="selectedFromDate"
            type="date"
            placeholder="From Date"
          />
        </div>

        <div class="col-md-3">
          <label :for="'activityThroughDate' + ucid">Through Date</label>
          <input
            :id="'activityThroughDate' + ucid"
            class="form-control"
            name="throughDate"
            v-model="selectedThroughDate"
            type="date"
            placeholder="Through Date"
          />
        </div>

        <div class="col-md-3">
          <label :for="'advancedSearch' + ucid"></label>
          <a
            data-toggle="collapse"
            :data-target="'#advancedSearch' + ucid + 'CollapsePanel'"
            role="button"
            aria-expanded="false"
            :aria-controls="'advancedSearch' + ucid + 'CollapsePanel'"
            @click.stop="toggleAdvancedSearch()"
          >
            Advanced Search
          </a>
        </div>
      </div>

      <div
        class="row mb-2 collapse"
        :id="'advancedSearch' + ucid + 'CollapsePanel'"
        v-bind:class="{ show: advancedSearch.show }"
      >
        <div class="card card-body">
          <div
            class="form-check form-check-inline"
            v-for="(column, index) in advancedSearch.availableColumnsToGraph"
            :key="index"
          >
            <input
              type="checkbox"
              :id="'advancedSearch' + ucid + 'ColumnsToChart' + column"
              class="form-check-input"
              :value="column"
              v-model="advancedSearch.selectedColumnsToGraph"
            />
            <label
              :for="'advancedSearch' + ucid + 'ColumnsToChart' + column"
              class="form-check-label"
              >{{ column }}</label
            >
          </div>
          <div>
            <input
              type="checkbox"
              :id="'advancedSearch' + ucid + 'FillInDates'"
              v-model="fillInDates"
            />
            <label :for="'advancedSearch' + ucid + 'FillInDates'">
              Fill in Dates
            </label>
          </div>
        </div>
      </div>
    </div>
  </div>
</template>

<script>
import ucid from '@/mixins/ucid';
import ActivitiesMixIn from '@/mixins/activities-mixin';

export default {
  name: 'ActivityChartViewFilter',
  mixins: [ucid, ActivitiesMixIn],
  props: {
    groupByDate: String,
    fromDate: String,
    throughDate: String,
    columnsToGraph: Array,
  },
  data() {
    return {
      selectedGroupByDate: this.groupByDate,
      selectedFromDate: this.fromDate,
      selectedThroughDate: this.throughDate,
      fillInDates: false,
      advancedSearch: {
        show: false,
        selectedColumnsToGraph: this.columnsToGraph,
        availableColumnsToGraph: ['efforts', 'distance', 'movingTime', 'elapsedTime', 'averageSpeed', 'maxSpeed', 'elevationGain', 'elevationHigh', 'elevationLow'],
      },
    };
  },
  updated() {
    this.filterUpdated();
  },
  methods: {
    toggleAdvancedSearch() {
      this.advancedSearch.show = !this.advancedSearch.show;
    },
    filterUpdated() {
      this.$emit('filterUpdated', {
        groupByDate: this.selectedGroupByDate,
        fromDate: this.selectedFromDate,
        throughDate: this.selectedThroughDate,
        columnsToGraph: this.advancedSearch.selectedColumnsToGraph,
        fillInDates: this.fillInDates,
      });
    },
  },
};
</script>
