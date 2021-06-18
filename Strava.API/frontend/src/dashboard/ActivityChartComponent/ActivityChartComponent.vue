<template>
  <div class="card">
    <div class="card-header"><slot /></div>
    <div class="card-body">
      <ActivityChartViewFilter
        :groupByDate="groupByDate"
        :fromDate="fromDate"
        :throughDate="throughDate"
        :columnsToGraph="columnsToGraph"
        @filterUpdated="filterUpdated"
      />
      <ActivityChartViewGraph
        :fromDate="selectedFromDate"
        :throughDate="selectedThroughDate"
        :type="type"
      />
      <ActivityChartView
        :groupByDate="groupByDate"
        :fromDate="fromDate"
        :throughDate="throughDate"
        :columnsToGraph="columnsToGraph"
        :fillInDates="fillInDates"
        :type="type"
        @chartLabelHoverChange="chartLabelHoverChange"
      />
    </div>
  </div>
</template>

<script>
import moment from 'moment';

import ucid from '@/mixins/ucid';
import ActivitiesMixIn from '@/mixins/activities-mixin';

import ActivityChartViewFilter from './ActivityChartViewFilter.vue';
import ActivityChartView from './ActivityChartView.vue';
import ActivityChartViewGraph from './ActivityChartViewGraph.vue';

export default {
  name: 'ActivityChartComponent',
  mixins: [ucid, ActivitiesMixIn],
  components: {
    ActivityChartViewFilter,
    ActivityChartView,
    ActivityChartViewGraph,
  },
  props: {
    type: String,
    columns: Array,
  },
  data() {
    return {
      groupByDate: 'Week',
      fromDate: moment().subtract(12, 'weeks').format('YYYY-MM-DD'),
      throughDate: null,
      columnsToGraph: this.columns,
      fillInDates: false,
      chartSelectedFromDate: null,
      chartSelectedThroughDate: null,
    };
  },
  computed: {
    selectedFromDate() {
      return this.chartSelectedFromDate ?? this.fromDate;
    },
    selectedThroughDate() {
      return this.chartSelectedThroughDate ?? this.throughDate;
    },
  },
  methods: {
    filterUpdated(data) {
      this.groupByDate = data.groupByDate;
      this.fromDate = data.fromDate;
      this.throughDate = data.throughDate;
      this.columnsToGraph = data.columnsToGraph;
      this.fillInDates = data.fillInDates;
    },
    chartLabelHoverChange(data) {
      this.chartSelectedFromDate = data.fromDate;
      this.chartSelectedThroughDate = data.throughDate;
    },
  },
};
</script>
