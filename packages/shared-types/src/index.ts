export type ModuleKey =
  | 'users'
  | 'payroll'
  | 'inventory'
  | 'products'
  | 'suppliers'
  | 'finance';

export interface ModuleDescriptor {
  key: ModuleKey;
  name: string;
  description: string;
}
