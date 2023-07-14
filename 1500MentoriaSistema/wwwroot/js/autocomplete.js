class Autocomplete {
    constructor(inputSelector, suggestionsSelector, valueInputSelector, optionsData) {
        this.input = document.querySelector(inputSelector);
        this.suggestionsList = document.querySelector(suggestionsSelector);
        this.valueInput = document.querySelector(valueInputSelector);
        this.backspace = false;
        this.options = optionsData;

        this.initialize();
    }

    initialize() {
        const self = this;

        for (let i = 0; i < this.options.length; i++) {
            if (this.options[i].value == this.valueInput.value) {
                this.input.value = this.options[i].text;
                this.valueInput.value = this.options[i].value;
                break;
            }
        }

        this.input.addEventListener('keydown', function (event) {
            if (event.keyCode !== 8) {
                self.backspace = false;
                self.searchHandler(event);
            } else {
                self.backspace = true;
                self.searchHandler(event);
                self.backspace = false;
            }
        });

        this.input.addEventListener('click', this.searchHandler.bind(this));
        this.suggestionsList.addEventListener('click', this.useSuggestion.bind(this));
        document.addEventListener('click', function (event) {
            if (event.target !== self.input && !self.input.contains(event.target)) {
                self.hideSuggestions();
            }
        });
    }

    search(str) {
        let results = [];
        const val = str.toLowerCase();

        for (let i = 0; i < this.options.length; i++) {
            if (this.options[i].text.toLowerCase().indexOf(val) > -1) {
                results.push({
                    value: this.options[i].value,
                    text: this.options[i].text
                });
            }
        }

        return results;
    }

    searchHandler(e) {
        let inputVal = e.currentTarget.value;
        if (e.keyCode >= 65 && e.keyCode <= 90) {
            inputVal = inputVal + e.key;
        } else if (this.backspace == true) {
            inputVal = inputVal.slice(0, -1);
        }
        let results = [];

        if (inputVal.length > 0) {
            results = this.search(inputVal);
        } else {
            results = this.options;
        }

        this.showSuggestions(results, inputVal);
    }

    hideSuggestions() {
        let haveValue = false;

        for (let i = 0; i < this.options.length; i++) {
            if (this.options[i].text.toUpperCase() == this.input.value.toUpperCase()) {
                haveValue = true;
            }
        }

        if (this.input.value.length > 0 && haveValue === false) {
            this.input.value = '';
            this.valueInput.value = 0;
        }

        this.suggestionsList.innerHTML = '';
        this.suggestionsList.classList.remove('has-suggestions');
    }

    showSuggestions(results, inputVal) {
        this.suggestionsList.innerHTML = '';

        if (results.length > 0) {
            for (let i = 0; i < results.length; i++) {
                let item = results[i].text;

                const match = item.match(new RegExp(inputVal, 'i'));
                item = item.replace(match[0], `<strong>${match[0]}</strong>`);
                this.suggestionsList.innerHTML += `<li data-value="${results[i].value}">${item}</li>`;
            }
            this.suggestionsList.classList.add('has-suggestions');
        } else {
            results = [];
            this.suggestionsList.innerHTML = '';
            this.suggestionsList.classList.remove('has-suggestions');
        }
    }

    useSuggestion(e) {
        const text = e.target.innerText;
        const value = e.target.dataset.value;
        this.input.value = text;
        this.valueInput.value = value;
        this.input.focus();
        this.suggestionsList.innerHTML = '';
        this.suggestionsList.classList.remove('has-suggestions');
    }
}