import { ComponentFixture, TestBed } from '@angular/core/testing';

import { AnnonceFormPage } from './annonce-form-page';

describe('AnnonceFormPage', () => {
  let component: AnnonceFormPage;
  let fixture: ComponentFixture<AnnonceFormPage>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [AnnonceFormPage],
    }).compileComponents();

    fixture = TestBed.createComponent(AnnonceFormPage);
    component = fixture.componentInstance;
    await fixture.whenStable();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
