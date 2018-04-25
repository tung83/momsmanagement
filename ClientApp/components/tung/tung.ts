import Vue from 'vue';
import { Component } from 'vue-property-decorator';

@Component
export default class TungComponent extends Vue {
    currentcount: number = 0;

    incrementCounter() {
        this.currentcount++;
    }
}
