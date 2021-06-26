<template id="WeightTrainingComponent">
  <div class="card">
    <div class="card-header" @click="show = !show">
      <div class="row">
        <div class="col fs-2">
          <i class="bi bi-arrows-collapse" v-show="this.show"></i>
          <i class="bi bi-arrows-expand" v-show="!this.show"></i>
        </div>
        <div class="col">
          <h2 class="text-center">Yogi</h2>
        </div>
        <div class="col fs-2">
          <i class="bi bi-arrows-collapse float-end" v-show="this.show"></i>
          <i class="bi bi-arrows-expand float-end" v-show="!this.show"></i>
        </div>
      </div>
    </div>
    <div class="card-body" v-show="show">
      <div class="row">
        <div class="col-xl-8">
          <ActivityChartComponent :type="type" v-bind:columns="columns" />
        </div>
        <div class="col-xl-4">
          <ActivityStatisticsComponents :type="type" />
        </div>
      </div>
    </div>
  </div>
</template>

<script>
import ActivityChartComponent from '@/dashboard/ActivityChartComponent/ActivityChartComponent.vue';
import ActivityStatisticsComponents from '@/dashboard/ActivityStatisticsComponent/ActivityStatisticsComponent.vue';

export default {
  name: 'WeightTrainingComponent',
  components: {
    ActivityChartComponent,
    ActivityStatisticsComponents,
  },
  data() {
    return {
      type: 'Yoga',
      columns: ['efforts'],
      show: false,
    };
  },
};
</script>
