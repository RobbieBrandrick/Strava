<template id="activity-charts">
  <div class="row">
    <div class="col-xl-6">
      <CardioView type="Run"> Review your running activities </CardioView>
    </div>
    <div class="col-xl-6">
      <CardioView type="Ride"> Review your cycling activities </CardioView>
    </div>
  </div>
</template>

<script>
import CardioView from './CardioView.vue';

export default {
  name: 'CardioViews',
  components: {
    CardioView,
  },
};
</script>
