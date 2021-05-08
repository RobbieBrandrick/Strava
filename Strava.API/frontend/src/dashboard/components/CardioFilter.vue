<template>
  <div class="form-row">
    <div class="form-group col-md-3">
      <label :for="'groupByDate' + ucid">Group By</label>
      <select
        :id="'groupByDate' + ucid"
        class="form-control"
        v-model="selectedGroupByDate"
      >
        <option>Day</option>
        <option>Week</option>
        <option>Month</option>
        <option>Year</option>
      </select>
    </div>

    <div class="form-group col-md-3">
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

    <div class="form-group col-md-3">
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

    <div class="form-group col-md-3">
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

    <div
      class="collapse"
      :id="'advancedSearch' + ucid + 'CollapsePanel'"
      v-bind:class="{ show: advancedSearch.show }"
    >
      <div class="card card-body">
        <div
          v-for="(column, index) in advancedSearch.availableColumnsToGraph"
          :key="index"
        >
          <input
            type="checkbox"
            :id="'advancedSearch' + ucid + 'ColumnsToChart' + column"
            :value="column"
            v-model="advancedSearch.selectedColumnsToGraph"
          />
          <label :for="'advancedSearch' + ucid + 'ColumnsToChart' + column">{{
            column
          }}</label>
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
</template>

<script>
import ucid from '@/mixins/ucid';
import getActivitiesMixin from '@/dashboard/components/activities-mixin';

export default {
  name: 'CardioViewFilter',
  mixins: [ucid, getActivitiesMixin],
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
        availableColumnsToGraph: ['distance', 'movingTime', 'elapsedTime', 'averageSpeed', 'maxSpeed', 'elevationGain', 'elevationHigh', 'elevationLow'],
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
