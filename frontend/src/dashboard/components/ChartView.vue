<template>
  <div>
    <canvas :id="chartId"></canvas>
  </div>
</template>

<script>
import Chart from 'chart.js';
import ucid from '../../mixins/ucid';

export default {
  name: 'ChartView',
  props: {
    chartData: Object,
  },
  mixins: [ucid],
  watch: {
    chartData() {
      this.render();
    },
  },
  data() {
    return {
      chart: null,
      chartId: `chart${this.ucid}`,
    };
  },
  methods: {
    render() {
      if (this.chart) {
        this.chart.destroy();
      }

      const ctx = document.getElementById(this.chartId).getContext('2d');

      this.chart = new Chart(ctx, this.chartData);
    },
  },
};
</script>
