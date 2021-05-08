<template>
  <div class="card">
    <div class="card-header"><slot /></div>
    <div class="card-body">
      <CardioFilter
        :groupByDate="groupByDate"
        :fromDate="fromDate"
        :throughDate="throughDate"
        :columnsToGraph="columnsToGraph"
        @filterUpdated="filterUpdated"
      />
      <CardioGraph
        :fromDate="selectedFromDate"
        :throughDate="selectedThroughDate"
        :type="type"
      />
      <CardioChart
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

import ucid from '../../mixins/ucid';
import getActivitiesMixin from './activities-mixin';

import CardioFilter from './CardioFilter.vue';
import CardioChart from './CardioChart.vue';
import CardioGraph from './CardioGraph.vue';

export default {
  name: 'CardioView',
  mixins: [ucid, getActivitiesMixin],
  components: {
    CardioFilter,
    CardioChart,
    CardioGraph,
  },
  props: {
    type: String,
  },
  data() {
    return {
      groupByDate: 'Week',
      fromDate: moment({ year: moment().year(), month: '0', day: '1' }).format('YYYY-MM-DD'),
      throughDate: null,
      columnsToGraph: ['distance', 'movingTime'],
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
