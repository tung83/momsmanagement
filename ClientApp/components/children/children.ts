import Vue from 'vue';
import { Component } from 'vue-property-decorator';

interface Children {
    id: string;
    name: string;
    address: string;
    birthday: Date;
    parent1: string;
    parent2: string;
}

@Component
export default class ChildrenComponent extends Vue {
    childrenList: Children[] = [];

    mounted() {
        fetch('api/Children')
            .then(response => response.json() as Promise<Children[]>)
            .then(data => {
                this.childrenList = data;
            });
    }
}
