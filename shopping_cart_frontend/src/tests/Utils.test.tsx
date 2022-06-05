import { formatNumber, createGuid } from '../shared/Utils';

describe('testing formatNumber method', () => {
    test('number returned as formatted string', () => {
        expect(formatNumber(2000.32)).toBe('2,000.32');
        expect(formatNumber(3122000.321111)).toBe('3,122,000.32');
    });
});

describe('testing creating Guid', () => {
    test('Guid return as fixed length string', () => {
        expect(createGuid()).toHaveLength(36);        
    });
});

