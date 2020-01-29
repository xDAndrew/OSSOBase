<template>
  <div class="tso-table-container">
    <v-toolbar class="tools-bar px-0" :flat="false" dense>
      <v-checkbox
        class="px-0"
        v-model="onlyMine"
        :label="`Только мои объекты`"
        :hide-details="true"
      ></v-checkbox>

      <v-spacer></v-spacer>

      <v-col cols="4" class="px-0">
        <v-text-field
          v-model="searchResult"
          append-icon="mdi-magnify"
          label="Поиск объекта"
          single-line
          hide-details
        ></v-text-field>
      </v-col>
    </v-toolbar>

    <v-card class="mx-auto my-card" max-width="100%" outlined>
      <v-btn fab color="cyan accent-2" top left absolute small @click="dialog = !dialog">
        <v-icon small>mdi-plus</v-icon>
      </v-btn>

      <v-data-table
        :headers="headers"
        :items="tsoList"
        class="elevation-1 rows-data-center"
        :disable-pagination="false"
        :hide-default-footer="true"
        :search="searchResult"
        :no-data-text="noData"
        :no-results-text="noDataResult"
        :fixed-header="true"
        :dense="true"
        :page="page"
        :items-per-page="itemsPerPage"
        @page-count="pageCount = $event"
      >
        <template v-slot:item.action="{ item }">
          <v-icon small class="auto-mx" @click="editItem(item)">mdi-pencil</v-icon>
        </template>
      </v-data-table>
    </v-card>

    <div class="text-center pt-2">
      <v-pagination v-model="page" :length="pageCount"></v-pagination>
    </div>

  </div>
</template>
<style scoped lang="scss" src="./TsoTable.component.scss"></style>
<script lang="ts" src="./TsoTable.component.ts"></script>