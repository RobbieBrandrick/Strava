<template>
  <div>
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
  },
  data() {
    return {
      chartData: {},
    };
  },
  computed: {
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

      for (let i = 0; i < this.columnsToGraph.length; i += 1) {
        const column = this.columnsToGraph[i];
        const group = this.groupBy()(activities, this.groupByDate, column);

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
        },
        options: {
          tooltips: {
            callbacks: {
              label(tooltipItem, data) {
                const datasetLabel = data.datasets[tooltipItem.datasetIndex].label;
                const datapointValue = data.datasets[tooltipItem.datasetIndex].data[tooltipItem.index];

                const time = ['movingTime', 'elapsedTime'];
                if (time.includes(datasetLabel)) {
                  return `${datasetLabel}: ${data.formatTime(datapointValue)}`;
                }

                const distance = ['distance'];
                if (distance.includes(datasetLabel)) {
                  return `${datasetLabel}: ${data.formatDistance(datapointValue)}`;
                }

                const speed = ['averageSpeed', 'maxSpeed'];
                if (speed.includes(datasetLabel)) {
                  return `${datasetLabel}: ${data.formatSpeed(datapointValue, data.cardioType)}`;
                }

                const elevation = ['elevationGain', 'elevationHigh', 'elevationLow'];
                if (elevation.includes(datasetLabel)) {
                  return `${datasetLabel}: ${data.formatElevation(datapointValue)}`;
                }

                return datapointValue;
              },
            },
          },
        },
      };

      return result;
    },
  },
};
</script>
