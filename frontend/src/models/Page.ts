
enum Language {
    "eng",
    "da"
}

export default interface IPage {
    title: string,
    url: string,
    language: Language,
    content: string,
    lastUpdated?: Date
}
