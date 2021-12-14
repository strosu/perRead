import { Component } from "react";

export class Articles extends Component {
    constructor(props) {
        super(props);
        this.state = { isLoading: true, articles: [] };
    }

    componentDidMount() {
        this.refreshData();
    }

    render() {
        let contents = this.state.isLoading ?
            "Loading data, patience" :
            this.renderData();

        return (
            <div>
                <h1 id="tabelLabel" >Articles</h1>
                <p>Loading articles...</p>
                {contents}
            </div>
        );
    }

    renderData() {
        return (
            <table className='table table-striped' aria-labelledby="tabelLabel">
                <thead>
                    <tr>
                        <th>Title</th>
                        <th>Author</th>
                        <th>Tags</th>
                        <th>Price</th>
                        <th>Summary</th>
                    </tr>
                </thead>
                <tbody>
                    {this.state.articles.map(article =>
                        <tr key={article.articleId}>
                            <td>{article.title}</td>
                            <td>{article.author}</td>
                            <td>{article.tags}</td>
                            <td>{article.price}</td>
                            <td>{article.summary}</td>
                        </tr>
                    )}
                </tbody>
            </table>
        );
    }

    async refreshData() {
        const response = await fetch('https://localhost:7176/article');
        const data = await response.json();
        this.setState({ articles: data, isLoading: false });
    }
}