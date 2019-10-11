import { HttpErrorResponse } from '@angular/common/http';

export class ServerError {
	public message: string;
	public status: number;
	public stackTrace: string;
	public originalError: HttpErrorResponse;
}
