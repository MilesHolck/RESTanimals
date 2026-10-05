{
    // Vælg ÉN baseUrl ad gangen:
    const baseUrl = "https://localhost:7070/api/Animals"                     // lokalt (API'et skal køre i Visual Studio)
    //const baseUrl = "https://<dit-app-navn>.azurewebsites.net/api/Animals" // på Azure

    Vue.createApp({
        data() {
            return {
                allAnimals: [],   // alt hvad API'et har sendt (røres ikke af filter)
                animals: [],      // det der vises i tabellen (kan være filtreret)
                name: null,
                color: null
            }
        },
        async created() { // life cycle method. Kaldes når siden indlæses
            this.getAll(baseUrl)
        },
        methods: {
            async getAll(url) {
                try {
                    const response = await axios.get(url)
                    // API'et svarer 204 NoContent (uden data) hvis listen er tom
                    this.allAnimals = response.status === 204 ? [] : response.data
                    this.animals = this.allAnimals
                    console.log(this.allAnimals)
                } catch (ex) {
                    alert(ex.message)
                }
            },

            // ---------- Sortering (i JavaScript) ----------
            sortById() {
                this.animals.sort((a1, a2) => a1.id - a2.id)
            },
            sortByName() {
                // localeCompare med "da" sorterer æ, ø, å korrekt
                this.animals.sort((a1, a2) => a1.name.localeCompare(a2.name, "da"))
            },
            sortByAgeAscending() {
                this.animals.sort((a1, a2) => a1.age - a2.age)
            },
            sortByAgeDescending() {
                this.animals.sort((a1, a2) => a2.age - a1.age)
            },
            sortByColor() {
                this.animals.sort((a1, a2) => a1.primaryColor.localeCompare(a2.primaryColor, "da"))
            },

            // ---------- Filtrering (i JavaScript) ----------
            filterByName(name) {
                if (!name) { // tomt felt -> vis alle
                    this.animals = this.allAnimals
                    return
                }
                this.animals = this.allAnimals.filter(a =>
                    a.name.toLowerCase().includes(name.toLowerCase()))
            },
            filterByColor(color) {
                if (!color) {
                    this.animals = this.allAnimals
                    return
                }
                this.animals = this.allAnimals.filter(a =>
                    a.primaryColor.toLowerCase().includes(color.toLowerCase()))
            },
            showAll() {
                this.name = null
                this.color = null
                this.animals = this.allAnimals
            }
        }
    }).mount("#app")
}