import React, { Component } from 'react';

export class ArticlePage extends Component
{
    constructor(props) {
        super(props);
        this.state = { articles: [], loading: true };
    }

    componentDidMount() {
        this.loadArticles();
    }

    async loadArticles() {
        const response = await fetch('api/articles');
        const data = await response.json();
        this.setState({ articles: data, loading: false });
    }

    renderArticles(articles) {
        return
        <table>
            <thead>
                <tr>
                    <th>Author</th>
                    <th>Title</th>
                    <th>Tags</th>
                    <th>Price</th>
                </tr>
            </thead>
            <tbody>
                {articles.map(article =>
                    <tr>
                        <td>{article.author}</td>
                        <td>{article.title}</td>
                        <td>{article.tags}</td>
                        <td>{article.price}</td>
                    </tr>
                )}
            </tbody>
        </table>
    }

    render() {
        let contents = this.state.loading
            ? <p><em>Loading...</em></p>
            : this.renderArticles(this.state.articles);

        return (
            <div>
                <h1 id="tabelLabel" >Created articles</h1>
                <p>This is a list of currently available articles.</p>
                {contents}
            </div>
        );
    }
}