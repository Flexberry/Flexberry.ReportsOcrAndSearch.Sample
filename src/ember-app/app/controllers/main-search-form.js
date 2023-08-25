import Controller from '@ember/controller';

export default Controller.extend({
    /**
        Текст для поиска.

        @property searchText
        @type String
        @default undefined
    */
    searchText: undefined,

    actions: {
        runSearch() {
            // вызов бекенда
        }
    }
});