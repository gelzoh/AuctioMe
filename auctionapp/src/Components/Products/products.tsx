import * as React from 'react';
import "./products.css"
import { config } from "../../ApiConfig"
import Product from "../Product/product"
import { UserProps } from "../../Common/types"
import { HubConnection, HubConnectionBuilder } from '@aspnet/signalr';

export default class Products extends React.PureComponent<{}>  {

    constructor(props: UserProps) {
        super(props);
        this.searchProducts();
    }

    state = {
        products: [],
        hubConnection: HubConnection.prototype,
        userName: ""
    }

    login() {
        const username = window.prompt('Enter a user name:', '');
        this.setState(prevState => ({
            ...prevState,
            userName: username ? username : "John"
        }));
    }

    registerProductUpdate() {
        const apiLocation = config.webapiHost + "auction";
        const hubConnection = new HubConnectionBuilder().withUrl(apiLocation).build();

        this.setState({ hubConnection }, () => {
            this.state.hubConnection.start()
                .then(() => console.log('Connection started!'))
                .catch(() => console.log('Error while establishing connection :('));
        });

    }

    searchProducts() {
        const apiLocation = config.productsApi + "product";
        fetch(apiLocation, {
            method: "GET",
            headers: {
                Accept: "application/json",
                "Content-Type": "application/json"
            }
        }).then(res => res.json())
            .then((data) => {
                console.log(data);
                this.setState((prevState) => ({
                    ...prevState,
                    products: data
                }));
                console.log(this.state);
            })
            .catch(console.log);
    }

    componentDidMount() {
        this.registerProductUpdate();
        this.login();// in a real scenario this wouldn't be here. Bad Gabriel!
    }

    render() {
        return (
            <div>
                <h1>AuctionMe</h1>
                <div className="products-list">
                    {(this.state.products !== undefined &&
                        this.state.products!.map((product, i) =>
                            <Product key={i} data={product} userName={this.state.userName} hubConnection={this.state.hubConnection} />))}
                </div>
            </div>

        )
    }
}