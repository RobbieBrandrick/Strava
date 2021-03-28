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
      <CardioChart
        :groupByDate="groupByDate"
        :fromDate="fromDate"
        :throughDate="throughDate"
        :columnsToGraph="columnsToGraph"
        :type="type"
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

export default {
  name: 'CardioView',
  mixins: [ucid, getActivitiesMixin],
  components: {
    CardioFilter,
    CardioChart,
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
    };
  },
  methods: {
    filterUpdated(data) {
      this.groupByDate = data.groupByDate;
      this.fromDate = data.fromDate;
      this.throughDate = data.throughDate;
      this.columnsToGraph = data.columnsToGraph;
    },
  },
};
</script>
