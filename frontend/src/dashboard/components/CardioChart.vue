<template>
  <div class="card">
    <div class="card-header"><slot /></div>
    <div class="card-body">
      <form :id="'updateChart' + ucid">
        <div class="form-row">
          <div class="form-group col-md-3">
            <label :for="'groupByDate' + ucid">Group By</label>
            <select
              :id="'groupByDate' + ucid"
              @change="renderChart"
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
              @change="renderChart"
              class="form-control"
              name="fromDate"
              v-model="fromDate"
              type="date"
              placeholder="From Date"
            />
          </div>

          <div class="form-group col-md-3">
            <label :for="'activityThroughDate' + ucid">Through Date</label>
            <input
              :id="'activityThroughDate' + ucid"
              @change="renderChart"
              class="form-control"
              name="throughDate"
              v-model="throughDate"
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
                v-for="column in advancedSearch.availableColumnsToGraph"
                :key="type + column"
              >
                <input
                  type="checkbox"
                  :id="'advancedSearch' + ucid + 'ColumnsToChart' + column"
                  :value="column"
                  v-model="advancedSearch.columnsToGraph"
                  @change="renderChart"
                />
                <label
                  :for="'advancedSearch' + ucid + 'ColumnsToChart' + column"
                  >{{ column }}</label
                >
              </div>
            </div>
          </div>
        </div>
      </form>

      <ChartView :chartData="chartData" />
    </div>
  </div>
</template>

<script>
import moment from 'moment';

import ucid from '../../mixins/ucid';
import getActivitiesMixin from './activities-mixin';
import ChartView from './ChartView.vue';

export default {
  name: 'CardioChart',
  mixins: [ucid, getActivitiesMixin],
  components: {
    ChartView,
  },
  props: {
    type: String,
    groupData: Function,
  },
  data() {
    return {
      selectedGroupByDate: 'Week',
      fromDate: moment({ year: moment().year(), month: '0', day: '1' }).format('YYYY-MM-DD'),
      throughDate: null,
      chartData: {},
      advancedSearch: {
        show: false,
        columnsToGraph: ['distance', 'movingTime'],
        availableColumnsToGraph: ['distance', 'movingTime', 'elapsedTime', 'averageSpeed', 'maxSpeed', 'elevationGain', 'elevationHigh', 'elevationLow'],
      },
    };
  },
  computed: {},
  mounted() {
    this.renderChart();
  },
  methods: {
    renderChart() {
      let { activities } = this;
      activities = activities.filter((e) => e.type === this.type);

      if (this.fromDate) {
        activities = activities.filter((activity) => activity.localDate.substr(0, 10) >= this.fromDate);
      }

      if (this.throughDate) {
        activities = activities.filter((activity) => activity.localDate.substr(0, 10) <= this.throughDate);
      }

      const chartData = [];
      const dates = new Set();

      for (let i = 0; i < this.advancedSearch.columnsToGraph.length; i += 1) {
        const column = this.advancedSearch.columnsToGraph[i];
        const group = this.groupData(activities, this.selectedGroupByDate, column);

        const columnDates = [...group.keys()];
        const columnData = [...group.values()];

        chartData.push({
          label: column,
          data: columnData,
        });

        columnDates.forEach((e) => {
          if (!dates.has(e)) dates.add(e);
        });
      }

      this.chartData = {
        // The type of chart we want to create
        type: 'line',
        data: {
          labels: [...dates],
          datasets: chartData,
        },
        options: {},
      };
    },
    toggleAdvancedSearch() {
      this.advancedSearch.show = !this.advancedSearch.show;
    },
  },
};
</script>
