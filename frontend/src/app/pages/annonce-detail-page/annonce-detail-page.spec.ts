import { ComponentFixture, TestBed } from '@angular/core/testing';

import { AnnonceDetailPage } from './annonce-detail-page';

describe('AnnonceDetailPage', () => {
  let component: AnnonceDetailPage;
  let fixture: ComponentFixture<AnnonceDetailPage>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [AnnonceDetailPage],
    }).compileComponents();

    fixture = TestBed.createComponent(AnnonceDetailPage);
    component = fixture.componentInstance;
    await fixture.whenStable();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
