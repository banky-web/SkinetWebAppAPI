import { HttpClient, HttpParams } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { IBrand } from '../shared/models/brand';
import { IPagination } from '../shared/models/pagination';
import { IProduct } from '../shared/models/product';
import { ShopParams } from '../shared/models/shopParams';
import { IType } from '../shared/models/type';
import { Observable, map, of } from 'rxjs';
import { observeNotification } from 'rxjs/internal/Notification';

@Injectable({
  providedIn: 'root'
})
export class ShopService {

  baseUrl = 'https://localhost:44352/api/';
  products: IProduct[] = [];
  brands: IBrand[] = [];
  types: IType[] = []; 
  pagination? : IPagination<IProduct[]>;
  shopParams = new ShopParams();
  productCache = new Map<string, IPagination<IProduct[]>>();


  constructor(private http: HttpClient) { }

  getProducts(useCache = true): Observable<IPagination<IProduct[]>> {

    if (!useCache) this.productCache = new Map();

    if (this.productCache.size > 0 && useCache){
      if (this.productCache.has(Object.values(this.shopParams).join('-'))){
        this.pagination = this.productCache.get(Object.values(this.shopParams).join('-'));
       if(this.pagination) return of(this.pagination);
      }
    }

    let params = new HttpParams();
    if (this.shopParams.brandId > 0) params = params.append('brandId', this.shopParams.brandId);
    if (this.shopParams.typeId) params = params.append('typeId', this.shopParams.typeId);
     params = params.append('sort', this.shopParams.sort);
     params = params.append('pageIndex', this.shopParams.pageNumber);
     params = params.append('pageSize', this.shopParams.pageSize);
    if(this.shopParams.search) params = params.append('search', this.shopParams.search);

    return this.http.get<IPagination<IProduct[]>>(this.baseUrl + 'products', {params}).pipe(
      map(response =>{
        this.productCache.set(Object.values(this.shopParams).join('-'), response)
        this.pagination = response;
        return response;
      })
    )
  }

    setShopParams(params: ShopParams){
      this.shopParams = params;
    }

    getShopParams(){
      return this.shopParams;
    }

  getproduct(id: number){
    const product = [...this.productCache.values()]
    .reduce((acc, paginatedResult) => {
      return {...acc, ...paginatedResult.data.find(x => x.id === id)}
    }, {} as IProduct)
   

    if (Object.keys(product).length !== 0) return of(product);

    return this.http.get<IProduct>(this.baseUrl + 'products/' + id);
  }

  getBrands(){
    if (this.brands.length > 0) return of(this.brands);

    return this.http.get<IBrand[]>(this.baseUrl + 'products/brands').pipe(
      map(brands => this.brands = brands)
    ); 
  }

   getTypes(){
    if (this.types.length > 0) return of(this.types);

    return this.http.get<IType[]>(this.baseUrl + 'products/types').pipe(
      map(types => this.types = types)
    ); 
  }
}
