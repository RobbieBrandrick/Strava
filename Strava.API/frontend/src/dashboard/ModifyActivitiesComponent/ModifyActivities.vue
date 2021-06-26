<template>
  <div class="card">
    <div class="card-header" @click="show = !show">
      <div class="row">
        <div class="col fs-2">
          <i class="bi bi-arrows-collapse" v-show="this.show"></i>
          <i class="bi bi-arrows-expand" v-show="!this.show"></i>
        </div>
        <div class="col">
          <h2 class="text-center">Modify Activities</h2>
        </div>
        <div class="col fs-2">
          <i class="bi bi-arrows-collapse float-end" v-show="this.show"></i>
          <i class="bi bi-arrows-expand float-end" v-show="!this.show"></i>
        </div>
      </div>
    </div>
    <div class="card-body" v-show="show">
      <div class="mb-4 col-md-3">
        <label for="activityType">Type</label>
        <select id="activityType" class="form-select" v-model="selectedType">
          <option :selected="true">All</option>
          <option v-for="type in types" :key="type" v-bind:value="type">
            {{ type }}
          </option>
        </select>
      </div>

      <div class="mb-3">
        <button class="btn btn-primary" @click="AddActivity">
          Add Activity
        </button>
      </div>

      <div class="mb-3">
        <button class="btn btn-primary" @click="RemoveActivity">
          Remove Activity
        </button>
      </div>

      <div class="mb-3">
        <button class="btn btn-primary" @click="RefreshActivities">
          Refresh Activities
        </button>
      </div>
    </div>
  </div>
</template>

<script>
import ActivitiesMixIn from '@/mixins/activities-mixin';

export default {
  name: 'ModifyActivities',
  mixins: [ActivitiesMixIn],
  data() {
    return {
      selectedType: 'Run',
      show: false,
    };
  },
  methods: {
    AddActivity() {
      const activity = JSON.parse(JSON.stringify(this.$store.state.activities.data.filter((e) => e.type === this.selectedType)[0]));
      activity.id += '1';
      this.$store.commit('activities/addActivities', [activity]);
    },
    RemoveActivity() {
      const activity = this.$store.state.activities.data.filter((e) => e.type === this.selectedType)[0];
      this.$store.commit('activities/removeActivities', [activity]);
    },
    RefreshActivities() {
      this.refreshActivities();
    },
  },
};
</script>
