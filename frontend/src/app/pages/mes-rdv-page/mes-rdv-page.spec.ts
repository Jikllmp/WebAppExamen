import { ComponentFixture, TestBed } from '@angular/core/testing';

import { MesRdvPage } from './mes-rdv-page';

describe('MesRdvPage', () => {
  let component: MesRdvPage;
  let fixture: ComponentFixture<MesRdvPage>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [MesRdvPage],
    }).compileComponents();

    fixture = TestBed.createComponent(MesRdvPage);
    component = fixture.componentInstance;
    await fixture.whenStable();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
