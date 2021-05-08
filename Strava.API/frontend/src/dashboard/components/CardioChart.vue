<template>
  <div>
    <button @click="afterSelectedChartLabel">Refresh</button>
    <ChartView :chartData="renderChart" />
  </div>
</template>

<script>
import ucid from '@/mixins/ucid';
import ChartView from '@/dashboard/components/ChartView.vue';
import getActivitiesMixin from '@/dashboard/components/activities-mixin';

export default {
  name: 'CardioChart',
  mixins: [ucid, getActivitiesMixin],
  components: {
    ChartView,
  },
  props: {
    groupByDate: String,
    fromDate: String,
    throughDate: String,
    columnsToGraph: Array,
    type: String,
    fillInDates: Boolean,
  },
  data() {
    return {
      chartData: {},
    };
  },
  computed: {
    renderChart() {
      const { activities } = this;
      let activitiesResult = activities.filter((e) => e.type === this.type);

      if (this.fromDate) {
        activitiesResult = activitiesResult.filter(
          (activity) => activity.localDate.substr(0, 10) >= this.fromDate,
        );
      }

      if (this.throughDate) {
        activitiesResult = activitiesResult.filter(
          (activity) => activity.localDate.substr(0, 10) <= this.throughDate,
        );
      }

      const chartData = [];
      const dates = new Set();

      for (let i = 0; i < this.columnsToGraph.length; i += 1) {
        const column = this.columnsToGraph[i];
        const group = this.groupBy()(activitiesResult, this.groupByDate, column, this.fillInDates);

        const columnDates = [...group.keys()];
        const columnData = [...group.values()].map((e) => e.data);

        chartData.push({
          label: column,
          data: columnData,
          raw: group,
        });

        columnDates.forEach((e) => {
          if (!dates.has(e)) dates.add(e);
        });
      }

      const result = {
        type: 'line',
        data: {
          labels: [...dates],
          datasets: chartData,
          cardioType: this.type,
          formatTime: this.formatTime(),
          formatDistance: this.formatDistance(),
          formatSpeed: this.formatSpeed(),
          formatElevation: this.formatElevation(),
          selectedChartLabel: this.selectedChartLabel,
          afterSelectedChartLabel: this.afterSelectedChartLabel,
        },
        options: {
          tooltips: {
            callbacks: {
              label(tooltipItem, data) {
                const dataset = data.datasets[tooltipItem.datasetIndex];
                const dataSetLabel = dataset.label;
                const toolTipValue = tooltipItem.value;
                const toolTipLabel = tooltipItem.label;
                const rawDataPoint = dataset.raw.get(toolTipLabel);

                data.selectedChartLabel(rawDataPoint);

                const time = ['movingTime', 'elapsedTime'];
                if (time.includes(dataSetLabel)) {
                  return `${dataSetLabel}: ${data.formatTime(toolTipValue)}`;
                }

                const distance = ['distance'];
                if (distance.includes(dataSetLabel)) {
                  return `${dataSetLabel}: ${data.formatDistance(toolTipValue)}`;
                }

                const speed = ['averageSpeed', 'maxSpeed'];
                if (speed.includes(dataSetLabel)) {
                  return `${dataSetLabel}: ${data.formatSpeed(toolTipValue, data.cardioType)}`;
                }

                const elevation = ['elevationGain', 'elevationHigh', 'elevationLow'];
                if (elevation.includes(dataSetLabel)) {
                  return `${dataSetLabel}: ${data.formatElevation(toolTipValue)}`;
                }

                return toolTipValue;
              },
            },
          },
        },
      };

      return result;
    },
  },
  methods: {
    selectedChartLabel(datapoint) {
      const { dates } = datapoint;

      const fromDate = dates.sort((lhs, rhs) => lhs > rhs)[0];
      const throughDate = dates.sort((lhs, rhs) => lhs < rhs)[0];

      this.$emit('chartLabelHoverChange', { fromDate, throughDate });
    },
    afterSelectedChartLabel() {
      this.$emit('chartLabelHoverChange', { fromDate: null, throughDate: null });
    },
  },
};
</script>
